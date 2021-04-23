const assert = require('chai').assert;
const doBFS = require('../../src/search/breadthFirstSearch').doBFS;

describe("Data Structures And Algorithms", () => {
    describe("Breadth-First Search", () => {
        it("search a graph", () => {
            const adjacencyList = [
                [1],
                [0, 4, 5],
                [3, 4, 5],
                [2, 6],
                [1, 2],
                [1, 2, 6],
                [3, 5],
                []
            ];
            const bfsInfo = doBFS(adjacencyList, 3);
            assert.deepEqual(bfsInfo[0], { distance: 4, predecessor: 1 });
            assert.deepEqual(bfsInfo[1], { distance: 3, predecessor: 4 });
            assert.deepEqual(bfsInfo[2], { distance: 1, predecessor: 3 });
            assert.deepEqual(bfsInfo[3], { distance: 0, predecessor: null });
            assert.deepEqual(bfsInfo[4], { distance: 2, predecessor: 2 });
            assert.deepEqual(bfsInfo[5], { distance: 2, predecessor: 2 });
            assert.deepEqual(bfsInfo[6], { distance: 1, predecessor: 3 });
            assert.deepEqual(bfsInfo[7], { distance: null, predecessor: null });
        });
    });
});