from pprint import pprint
import sys

from pathlib import Path
from typing import Dict, List

from pyvis.network import Network


def _parse_text(text: str) -> Dict[str, List[str]]:
    data: Dict[str, List[str]] = {}
    dependencies: List[str] = None
    for line in text.splitlines():
        if not line.strip():
            continue
        if not line.startswith((' ', '\t')):
            data[line.strip()] = dependencies = []
        else:
            dependencies.append(line.strip())
    return data


def _parse_file(path: Path):
    return _parse_text(path.read_text())


def main(file_path: str):
    path = Path(file_path).resolve()
    if not path.exists():
        raise FileNotFoundError(str(path))
    data = _parse_file(path)
    net = Network("100%", "100%")
    for key in data:
        net.add_node(key)
    for key, values in data.items():
        for value in values:
            net.add_edge(key, value, title=f"{key} -> {value}")
    net.show(path.with_suffix(".html").name)
    


if __name__ == '__main__':
    try:
        filename = sys.argv[1]
    except IndexError:
        print(f"Usage: {__file__} <filename>")
    else:
        main(filename)
