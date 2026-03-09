#!/usr/bin/env python3
import json
import sys
from pathlib import Path
import argparse

try:
    import jsonschema
    from jsonschema import Draft7Validator
except Exception as e:
    print("Missing dependency 'jsonschema'. Install with: pip install -r requirements.txt", file=sys.stderr)
    raise


def load_schema(schema_path: Path):
    with schema_path.open("r", encoding="utf-8") as f:
        return json.load(f)


def validate_file(data_path: Path, schema):
    with data_path.open("r", encoding="utf-8") as f:
        try:
            data = json.load(f)
        except Exception as e:
            return False, f"JSON parse error: {e}"

    if "rubric" not in data:
        return False, "'rubric' object missing"

    validator = Draft7Validator(schema)
    errors = sorted(validator.iter_errors(data["rubric"]), key=lambda e: e.path)
    if errors:
        messages = []
        for err in errors:
            path = ".".join(map(str, err.absolute_path)) or "(root)"
            messages.append(f"{path}: {err.message}")
        return False, "; ".join(messages)
    return True, "OK"


def find_data_jsons(root: Path):
    for p in root.rglob("data.json"):
        yield p


def main():
    parser = argparse.ArgumentParser(description="Validate rubric objects in data.json files against rubric_schema.json")
    parser.add_argument("--dir", "-d", default=".", help="Root folder to search (default: current dir)")
    parser.add_argument("--schema", "-s", default="rubric_schema.json", help="Schema file path (default: rubric_schema.json next to the script)")
    args = parser.parse_args()

    script_dir = Path(__file__).resolve().parent
    root = Path(args.dir).resolve()
    schema_path = (Path(args.schema).resolve()
                   if Path(args.schema).is_absolute()
                   else script_dir.joinpath(args.schema))

    if not schema_path.exists():
        print(f"Schema not found at {schema_path}", file=sys.stderr)
        sys.exit(2)

    schema = load_schema(schema_path)

    total = 0
    valid_count = 0
    invalid_count = 0
    for data_path in find_data_jsons(root):
        total += 1
        ok, msg = validate_file(data_path, schema)
        rel = data_path.relative_to(root)
        if ok:
            valid_count += 1
            print(f"[OK]     {rel}")
        else:
            invalid_count += 1
            print(f"[INVALID] {rel} -> {msg}")

    print(f"\nChecked: {total} files — valid: {valid_count}, invalid: {invalid_count}")
    sys.exit(0 if invalid_count == 0 else 1)


if __name__ == "__main__":
    main()
