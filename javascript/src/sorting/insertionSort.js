/**
 * Move elements of arr[0..rightIndex] that are greater than value and insert the given value to the right position.
 * @param {Number[]} arr The array in which insert opeartion will occur.
 * @param {Number} rightIndex The end index of sub-array.
 * @param {Number} value The value to be inserted in the right place.
 */
const insert = (arr, rightIndex, value) => {
    for (var i = rightIndex; i >= 0 && arr[i] > value; i--) {
        arr[i + 1] = arr[i];
    }
    arr[i + 1] = value;
}
/**
 * Sort the given array by insertionsort.    
 * @param {Number[]} arr The array to be sorted.
 */
const insertionSort = (arr) => {
    for (let i = 1; i < arr.length; i++) {
        insert(arr, i - 1, arr[i]);
    }
}
exports.insertionSort = insertionSort;