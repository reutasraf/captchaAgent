import json, sys


def calc_beta(player_score, bob_score):
    diff = bob_score - player_score
    beta = 1 + diff / 8
    return max(beta, 0.1)


def estimated_pace(alpha, old_pace, interval, num_of_succ):
    if num_of_succ == 0:
        num_of_succ = 1
    return (1-alpha) * old_pace + alpha * interval / num_of_succ


if __name__ == '__main__':
    s = sys.argv[1]
    s = s.replace('\'', '\"')
    info = json.loads(s)

    alpha = 1/4
    old_pace = int(info['pace'])
    interval = int(info['interval'])
    num_of_succ = int(info['CountChecker'].split(',')[0])
    player_score = int(info['NumOfSucssess'])
    bob_score = int(info['BobNumOfSucssess'])

    pace = estimated_pace(alpha, old_pace, interval, num_of_succ)
    pace *= calc_beta(player_score, bob_score)
    sys.stdout.write(str(int(pace)))
    sys.stdout.flush()
    sys.exit(0)
