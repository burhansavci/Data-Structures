/**
 * Swaps two items in an array.
 * @param {Number[]} arr The array where the swap will occur.
 * @param {Number} firstIndex The first index to be used in swap process.
 * @param {Number} secondIndex The second index to be used in swap process.
 */
function swap(arr, firstIndex, secondIndex) {
    const temp = arr[firstIndex];
    arr[firstIndex] = arr[secondIndex];
    arr[secondIndex] = temp;
}

/**
 * Finds the median of three and returns it
 * @param {Number} a The number to be compared with the others.
 * @param {Number} b The number to be compared with the others.
 * @param {Number} c The number to be compared with the others.
 * @returns {Number} Median of three
 */
function median(a, b, c) {
    if (c < a) {
        if (a < b) return a; //c<a and a<b then c<a<b
        else if (c < b) return b; //c<b and b<a then c<b<a
        else return c; //b<c and c<a then b<c<a
    }
    else {
        if (b < a) return a; //b<a and a<c then b<a<c
        else if (b < c) return b; //a<b and b<c then a<b<c
        else return c; //a<c and c<b then a<c<b
    }
}
/**
 *  Gets the random pivot position with the specified range.
 * @param {Number} left The start point of specified the range.
 * @param {Number} right The end point of the specified range.
 * @returns {Number} Random pivot position
 */
function getPivotPosition(left, right) {
    return median(getRandomInt(left, right), getRandomInt(left, right), getRandomInt(left, right));
}

/**
 * Returns a random number between min (inclusive) and max (exclusive)
 * @param {Number} min min (inclusive)
 * @param {Number} max max (exclusive)
 * @returns {Number} random number
 */
function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min) + min);
}

/**
 * Partitions the given array and returns the index of the pivot.
 * @param {Number[]} arr The array to be partitioned.
 * @param {Number} left The start point of the partitioning.
 * @param {Number} right The end point of the partitioning.
 * @returns {Number} index of the pivot
 */
function partition(arr, left, right) {

    const pivotPosition = getPivotPosition(left, right);
    swap(arr, pivotPosition, right);

    const pivot = arr[right];
    let newPivotPosition = left;

    for (let i = left; i < right; i++) {
        if (arr[i] <= pivot) {
            swap(arr, i, newPivotPosition);
            newPivotPosition++;
        }
    }

    swap(arr, newPivotPosition, right);
    return newPivotPosition;
}

/**
 * Recursively find the pivot and sort the given array, dividing each array as it recurses.
 * @param {Number[]} arr The array to be sorted.
 * @param {Number} left The start point of the sorting.
 * @param {Number} right The end point of the sorting.
 */
const quickSortOptimized = (arr, left, right) => {
    if (typeof left === 'undefined' && typeof right === 'undefined') {
        quickSortOptimized(arr, 0, arr.length - 1)
    } else {
        if (left < right) {
            const pivot = partition(arr, left, right)
            quickSortOptimized(arr, left, pivot - 1);
            quickSortOptimized(arr, pivot + 1, right);
        }
    }
}

exports.quickSortOptimized = quickSortOptimized;