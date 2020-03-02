const cache = new Map();

export function set(key, value) {
    cache.set(key, value);
}

export function get(key) {
    return cache.get(key);
}

export function has(key) {
    return cache.has(key);
}

export function clear() {
    cache.clear();
}
