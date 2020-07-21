import { run } from 'math';

const v = run();

console.dir(v);

assert(v.pi === Math.PI, `Should be pi but is: ${v.pi}`);
