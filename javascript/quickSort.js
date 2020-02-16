/**
 * Swaps two items in an array.
 * @param {Number[]} arr The array where the swap will occur.
 * @param {Number} firstIndex The first index to be used in swap process.
 * @param {Number} secondIndex The second index to be used in swap process.
 */
const swap = (arr, firstIndex, secondIndex) => {
    const temp = arr[firstIndex];
    arr[firstIndex] = arr[secondIndex];
    arr[secondIndex] = temp;
}

/**
 * Partitions the given array and returns the index of the pivot.
 * @param {Number[]} arr The array to be partitioned.
 * @param {Number} left The start point of the partitioning.
 * @param {Number} right The end point of the partitioning.
 * @returns {Number} index of the pivot
 */
const partition = (arr, left, right) => {
    const pivot = arr[right];
    let pivotPosition = left;
    for (let i = left; i < right; i++) {
        if (arr[i] <= pivot) {
            swap(arr, pivotPosition, i);
            pivotPosition++;
        }
    }
    swap(arr, pivotPosition, right);
    return pivotPosition;
}

/**
 * Recursively find the pivot and sort the given array, dividing each array as it recurses.
 * @param {Number[]} arr The array to be sorted.
 * @param {Number} left The start point of the sorting.
 * @param {Number} right The end point of the sorting.
 */
const quickSort = (arr, left, right) => {
    if (typeof left === 'undefined' && typeof right === 'undefined') {
        quickSort(arr, 0, arr.length - 1);
    } else {
        if (left < right) {
            const pivot = partition(arr, left, right);
            quickSort(arr, left, pivot - 1);
            quickSort(arr, pivot + 1, right);
        }
    }
}

exports.swap = swap;
exports.partition = partition;
exports.quickSort = quickSort;