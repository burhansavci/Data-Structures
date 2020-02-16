const assert = require('chai').assert;
const quickSort = require('../quickSort').quickSort;

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
});