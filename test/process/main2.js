function run() {
    const pi = Math.PI;
    const e = Math.E;

    return { pi: pi, e };
}


const v = run();

console.dir(v);

assert(v.pi === Math.PI, `Should be pi but is: ${v.pi}`);
assert(v.e === Math.E, `Should be e but is: ${v.e}`);
