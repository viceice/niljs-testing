import * as math from 'math';
//import { random, cache } from 'utils';

//res = 'todo';
//log('before');
//function prepare() {
//    log('start');

//    log('step');

//    log('item:' + typeof item);

//    if (typeof item !== 'undefined') {
//        log('have item');
//        log('item.Test:' + item.Test);
//        log('item.Title:' + item.Title);
//        log('assert:' + typeof assert);

//        res = item.Title;
//        //assert(item.test === 5, "should be 5");
//        log('have item end');
//    }

//    log('end');
//}

//export default function() {
//    res = item.Title;
//    item.Title = 'test';
//    log('execute default');
//    item.Update({ Mode: 'Delete', arr: [1, '2ddd'], obj: { key: 'test' } });
//    assert(typeof item.update === 'undefined', 'update should be undefined');

//    assert(item.get_Item('Test') === 5, 'Test should be 5');
//    const d = Dict.from();
//    d.set_Item('Test', 5);
//    d.set_Item('Overwrite', true);

//    item.Create(d);

//    item.Create(Dict.from({ FileLeafRef: 'doc.docx', arr: [1, '2ddd'], obj: { key: 'test' } }));

//    Dict.set(item, 'Test', 1);
//    Dict.set(item, 'rand', random());

//    assert(cache.get('PI') === math.Pi, 'PI should be cached');
//    cache.clear();
//    assert(cache.has('PI') === false, 'PI should not cached');

//    assert(typeof math.PI === 'number', 'Pi should be a number');
//    assert(math.PI > 3.414, 'Pi should be bigger than 3');

//    return math.PI;
//}

//prepare();

//log('after');

//assert(typeof item === 'undefined', 'should be undefined');

//assert(typeof Promise === 'undefined', 'Promise should be undefined');
//assert(typeof Dict.Get === 'undefined', 'Dict.Get should be undefined');

//assert(cache.has('PI') === false, 'PI should not cached');
//math.test();

assert(typeof math.PI === 'number', 'Pi should be a number');
