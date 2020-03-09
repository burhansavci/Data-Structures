const assert = require('chai').assert;
const quickSort = require('../quickSort').quickSort;
const quickSortOptimized = require('../quickSortOptimized').quickSortOptimized;
const selectionSort = require('../selectionSort').selectionSort;
const insertionSort = require('../insertionSort').insertionSort;
const mergeSort = require('../mergeSort').mergeSort;

describe("Data Structures And Algorithms", () => {
    describe("Quick Sort", () => {
        it("sorts an array in place", () => {
            const arr1 = [10, 9, 8, 7, 6, 5, 4, 3, 2, 1];
            const arr2 = [56, 34, 103, -1, 11, 0, -23, 10, 75, 103];
            const arr3 = [-12, -15, -6, -3, -2, 0, -23, -13, -75];
            quickSort(arr1);
            assert.deepEqual(arr1, [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);
            quickSort(arr2);
            assert.deepEqual(arr2, [-23, -1, 0, 10, 11, 34, 56, 75, 103, 103]);
            quickSort(arr3);
            assert.deepEqual(arr3, [-75, -23, -15, -13, -12, -6, -3, -2, 0]);
        });
    });
    describe("Quick Sort Optimized", () => {
        it("sorts an array in place", () => {
            const arr1 = [10, 9, 8, 7, 6, 5, 4, 3, 2, 1];
            const arr2 = [56, 34, 103, -1, 11, 0, -23, 10, 75, 103];
            const arr3 = [-12, -15, -6, -3, -2, 0, -23, -13, -75];
            quickSortOptimized(arr1);
            assert.deepEqual(arr1, [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);
            quickSortOptimized(arr2);
            assert.deepEqual(arr2, [-23, -1, 0, 10, 11, 34, 56, 75, 103, 103]);
            quickSortOptimized(arr3);
            assert.deepEqual(arr3, [-75, -23, -15, -13, -12, -6, -3, -2, 0]);
        });
    });
    describe("Selection Sort", () => {
        it("sorts an array in place", () => {
            const arr1 = [3, 4, 5, 1, 2, -23, -10];
            const arr2 = [9, 2, 3, 2, 4, 3, -10, 55, -10, 0];
            const arr3 = [100, 99, 5, 4, 3, 2, 1, 0, -10, -78];
            selectionSort(arr1);
            selectionSort(arr2);
            selectionSort(arr3);
            assert.deepEqual(arr1, [-23, -10, 1, 2, 3, 4, 5]);
            assert.deepEqual(arr2, [-10, -10, 0, 2, 2, 3, 3, 4, 9, 55]);
            assert.deepEqual(arr3, [-78, -10, 0, 1, 2, 3, 4, 5, 99, 100]);
        });
    });
    describe("Insertion Sort", () => {
        it("sorts an array in place", () => {
            const arr1 = [3, 4, 5, 1, 2, -23, -10];
            const arr2 = [9, 2, 3, 2, 4, 3, -10, 55, -10, 0];
            const arr3 = [100, 99, 5, 4, 3, 2, 1, 0, -10, -78];
            insertionSort(arr1);
            insertionSort(arr2);
            insertionSort(arr3);
            assert.deepEqual(arr1, [-23, -10, 1, 2, 3, 4, 5]);
            assert.deepEqual(arr2, [-10, -10, 0, 2, 2, 3, 3, 4, 9, 55]);
            assert.deepEqual(arr3, [-78, -10, 0, 1, 2, 3, 4, 5, 99, 100]);
        });
    });
    describe("Merge Sort", () => {
        it("sorts an array in place", () => {
            const arr1 = [3, 4, 5, 1, 2, -23, -10];
            const arr2 = [9, 2, 3, 2, 4, 3, -10, 55, -10, 0];
            const arr3 = [100, 99, 5, 4, 3, 2, 1, 0, -10, -78];
            mergeSort(arr1);
            mergeSort(arr2);
            mergeSort(arr3);
            assert.deepEqual(arr1, [-23, -10, 1, 2, 3, 4, 5]);
            assert.deepEqual(arr2, [-10, -10, 0, 2, 2, 3, 3, 4, 9, 55]);
            assert.deepEqual(arr3, [-78, -10, 0, 1, 2, 3, 4, 5, 99, 100]);
        });
    });
});