import json
import sys

import staticModel as model


if __name__ == '__main__':
    s = sys.argv[1]
    if s == 'GETTYPE':
        sys.stdout.write(str(model.get_experiment_type()))
    else:
        s = s.replace('\'', '\"')
        info = json.loads(s)
        sys.stdout.write(str(int(model.get_pace(info))))

    sys.stdout.flush()
    sys.exit(0)
