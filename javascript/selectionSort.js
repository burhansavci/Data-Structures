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
 * Find the index of the minimum number by looping through the sub-array created from the start index.
 * @param {Number[]} arr The array in which the loop will take place.
 * @param {Number} startIndex The start index of the sub-array.
 * @returns {Number} The index of the minimum number. 
 */
const indexOfMinimum = (arr, startIndex) => {
    let minValue = arr[startIndex];
    let minIndex = startIndex;
    for (let i = minIndex + 1; i < arr.length; i++) {
        if (arr[i] < minValue) {
            minValue = arr[i];
            minIndex = i;
        }
    }
    return minIndex;
}

/**
 * Sort the given array by selectionsort.
 * @param {Number[]} arr The array to be sorted.
 */
const selectionSort = (arr) => {
    let minIndex;
    for (let i = 0; i < arr.length; i++) {
        minIndex = indexOfMinimum(arr, i);
        swap(arr, minIndex, i);
    }
}

exports.selectionSort=selectionSort;
