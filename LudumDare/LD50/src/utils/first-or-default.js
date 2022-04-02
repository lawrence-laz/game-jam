const firstOrDefault = (array, predicate) => {
    
    for (var i = 0; i < array.length; ++i) {
        if (predicate(array[i])) {
            return array[i];
        }
    }

    return null;
}

export default firstOrDefault;
