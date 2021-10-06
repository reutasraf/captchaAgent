import json, sys, re

import FirstStaticPacer as FSP


def parse_timeline(t):
    event_pattern = r'[S]-[0-9]+:[0-9]+:[0-9]+'
    return re.findall(event_pattern, t)


def parse_event(e):
    mults = [3600, 60, 1]
    return sum([a * int(b) for a, b in zip(mults, re.findall(r'[0-9]+', e))])


def get_events(timeline):
    return [parse_event(e) for e in parse_timeline(timeline)]


# get events in seconds
def norm_events(events, start_point):
    return [e - start_point for e in events]


def get_avg_pace(normed_events, time_elapsed, range_of_seconds):
    num_of_succ = len([e for e in normed_events if e > time_elapsed - range_of_seconds])
    if num_of_succ == 0:
        return 20000
    return (range_of_seconds  * 1000) // num_of_succ


def change_pace_bounded(old_pace, new_pace, start_time, end_time, curr_time):
    alpha = (curr_time - start_time) / (end_time - start_time)
    return int((1 - alpha) * old_pace + alpha * new_pace)


def main_stage_pace(info, start_main_stage, end_main_stage):
    start_point = info['StartPoint']
    elapsed_time = int(info['ElapsedTime'])
    time_line = info['Timeline']
    player_score = int(info['NumOfSucssess'])
    bob_score = int(info['BobNumOfSucssess'])
    old_pace = int(info['pace'])

    events = get_events(time_line)
    events = norm_events(events, parse_event(start_point))

    # Todo find the optimal way to calc evg
    avg_pace = get_avg_pace(events, elapsed_time, 30)

    new_pace = old_pace

    # if pacer is leading
    if player_score <= bob_score:
        # if the pacer isn't slow enough
        # Todo find the best factor for the slower pace
        if old_pace < 1.2 * avg_pace:
            # Todo find the best factor for slowing the pace
            new_pace = old_pace * 1.1

    # if the player is leading by 2 or more
    elif player_score > bob_score + 1:
        # if the pacer isn't fast enough
        if old_pace > 0.8 * avg_pace:
            new_pace = old_pace * 0.9

    return int(new_pace)


def final_stage_pace(info, start_final_stage, end_final_stage):
    start_point = info['StartPoint']
    elapsed_time = int(info['ElapsedTime'])
    time_line = info['Timeline']

    events = get_events(time_line)
    events = norm_events(events, parse_event(start_point))

    # Todo find the optimal way to calc evg
    avg_pace = get_avg_pace(events, elapsed_time, 30)

    # Todo find to optimal final stage factor
    return int(change_pace_bounded(avg_pace, avg_pace * 0.6,
                               start_final_stage, end_final_stage, elapsed_time))


def get_pace(arg):
    start_main_stage = 60 * 2
    start_final_stage = 60 * 8
    end_final_stage = 60 * 10

    s = arg
    s = s.replace('\'', '\"')
    info = json.loads(s)

    #  info = {}
    #  with open('info.txt', 'r') as f:
    #      for line in f:
    #          k, v = line.strip().split('&')
    #          info[k] = v

    # with open("C:/inetpub/wwwroot/EmptyPacer4/Models/debug.txt", "a") as myfile:
    #     for k, v in info.items():
    #         myfile.write('{}: {}\n'.format(k, v))

    elapsed_time = int(info['ElapsedTime'])
    if elapsed_time < start_main_stage:
        pace = 7000
    elif elapsed_time < end_final_stage:
        pace = main_stage_pace(info, start_main_stage, start_final_stage)
    else:
        pace = final_stage_pace(info, start_final_stage, end_final_stage)

    # with open("C:/inetpub/wwwroot/EmptyPacer4/Models/debug.txt", "a") as myfile:
    #     myfile.write('{}\n'.format(pace))
    # sys.stdout.write(str(int(pace)))
    # sys.stdout.flush()
    # sys.exit(0)
    return int(pace)


# def get_experiment_type():
#     return "Covid-19"


if __name__ == '__main__':
    arg = sys.argv[1]
    if arg == 'GETTYPE':
        sys.stdout.write(str(FSP.get_experiment_type()))
    else:
        sys.stdout.write(str(int(get_pace(arg))))

    sys.stdout.flush()
    sys.exit(0)
