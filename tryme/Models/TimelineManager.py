import re

def parse_timeline(t):
    event_pattern = r'[SF]-[0-9]+:[0-9]+:[0-9]+'
    return re.findall(event_pattern, t)


def parse_event(e):
    mults = [3600, 60, 1]
    return e[0], sum([a * int(b) for a, b in zip(mults, re.findall(r'[0-9]+', e))])


def get_events(timeline):
    return  [parse_event(e) for e in parse_timeline(timeline)]


def get_progress(pe,time):
    d = {'S': 1, 'F': 0}
    times = [e[1] for e in pe]
    # check for new day
    for i in range(1, len(times)):
        if times[i] < times[i - 1]:
            times[i] += 12*60*60
    # mintimes = min(times)
    maxtimes = times[-1]
    times = [x - maxtimes + time for x in times]
    y = [d[pe[0][0]]]
    for i, e in enumerate(pe[1:], start=1):
        y.append(y[i - 1] + d[e[0]])
    return times, y


def fill(times, y, max):
    length = len(times)
    new_times = list(range(max + 1))
    new_y = [0] * (max + 1)
    idx = 0
    for i in range(times[idx], max + 1):
        if idx >= length:
            new_y[i] = y[idx - 1]
            continue
        if i >= times[idx]:
            idx += 1
        new_y[i] = y[idx - 1]

    return new_times, new_y



