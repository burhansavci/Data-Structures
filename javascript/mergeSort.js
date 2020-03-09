/**
 * Takes in an array that has two sorted subarrays, from [lowHalfStart..pivot] and [pivot+1..highHalfEnd], and merges the array.
 * @param {Number[]} arr The array to be merged.
 * @param {Number} lowHalfStart The start index of lower half of the given array.
 * @param {Number} pivot The middle index of the given array.
 * @param {Number} highHalfEnd The end index of higher half of the given array.
 */
const merge = (arr, lowHalfStart, pivot, highHalfEnd) => {

    let lowHalf = [];
    let highHalf = [];
    let arrPointer = lowHalfStart;
    //Copy the given array into lowHalf and highHalf. 
    for (let i = 0; arrPointer <= pivot; arrPointer++ , i++) {
        lowHalf[i] = arr[arrPointer];
    }
    for (let i = 0; arrPointer <= highHalfEnd; arrPointer++ , i++) {
        highHalf[i] = arr[arrPointer];
    }

    arrPointer = lowHalfStart;
    let lowHalfPointer = 0;
    let highHalfPointer = 0;
    // Repeatedly compare the lowest untaken element in
    // lowHalf with the lowest untaken element in highHalf
    // and copy the lower of the two back into arr.
    while (lowHalfPointer < lowHalf.length && highHalfPointer < highHalf.length) {
        if (lowHalf[lowHalfPointer] < highHalf[highHalfPointer]) {
            arr[arrPointer] = lowHalf[lowHalfPointer];
            lowHalfPointer++
        } else {
            arr[arrPointer] = highHalf[highHalfPointer];
            highHalfPointer++;
        }
        arrPointer++;
    }

    // Once one of lowHalf and highHalf has been fully copied
    // back into arr, copy the remaining elements from the
    // other temporary array back into the arr.
    while (lowHalfPointer < lowHalf.length) {
        arr[arrPointer] = lowHalf[lowHalfPointer];
        lowHalfPointer++
        arrPointer++;
    }
    while (highHalfPointer < highHalf.length) {
        arr[arrPointer] = highHalf[highHalfPointer];
        highHalfPointer++
        arrPointer++;
    }

}

/**
 * Recursively calculate the middle pivot and sort the given array, merging each array as it recurses.
 * @param {Number[]} arr >The array to be sorted.
 * @param {Number} lowHalfStart The start index of lower half of the given array.
 * @param {Number} highHalfEnd The end index of higher half of the given array.
 */
const mergeSort = (arr, lowHalfStart, highHalfEnd) => {
    if (typeof lowHalfStart === 'undefined' && typeof highHalfEnd === 'undefined') {
        mergeSort(arr, 0, arr.length - 1);
    } else {
        if (lowHalfStart < highHalfEnd) {
            let pivot = Math.floor((lowHalfStart + highHalfEnd) / 2);
            mergeSort(arr, lowHalfStart, pivot);
            mergeSort(arr, pivot + 1, highHalfEnd);
            merge(arr, lowHalfStart, pivot, highHalfEnd);
        }
    }
}

exports.mergeSort = mergeSort;
