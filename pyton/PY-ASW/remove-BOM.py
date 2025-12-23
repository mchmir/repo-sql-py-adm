import pathlib


root = pathlib.Path(r"C:\septim.git\Septim4.limited\Septim\PgSQL\Proc")
for p in root.rglob("*.tp"):
    b = p.read_bytes()
    if b.startswith(b"\xEF\xBB\xBF"):
        p.write_bytes(b[3:])
        print("Fixed:", p)
