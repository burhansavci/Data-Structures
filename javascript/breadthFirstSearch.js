// A Queue object for queue-like functionality over JavaScript arrays.
class Queue {
    constructor() {
        this.items = [];
    }
    enqueue(obj) {
        this.items.push(obj);
    }
    dequeue() {
        return this.items.shift();
    }
    isEmpty() {
        return this.items.length === 0;
    }
}

/**
 * Performs a breadth-first search on a graph
 * @param {Number[][]} graph - Graph, represented as adjacency lists.
 * @param {Number} source - The index of the source vertex.
 * @returns {Object[]} Array of objects describing each vertex, like [{distance: _, predecessor: _ }] 
 */
const doBFS = (graph, source) => {
    let bfsInfo = [];
    //Initially set the distance and predecessor of each vertex to null
    for (let i = 0; i < graph.length; i++) {
        bfsInfo[i] = {
            distance: null,
            predecessor: null
        }
    }

    // Assign source a distance of 0 and enqueue it to the queue.
    bfsInfo[source].distance = 0;
    let queue = new Queue();
    queue.enqueue(source);

    // Traverse the graph as long as the queue is not empty.
    while (!queue.isEmpty()) {
        // Repeatedly dequeue a vertex u from the queue.
        let u = queue.dequeue();
        for (let i = 0; i < graph[u].length; i++) {
            let v = graph[u][i];
            // For each neighbor vertex v of vertex u that has not been visited:
            if (bfsInfo[v].distance === null) {
                // Set distance to 1 greater than vertex u's distance
                bfsInfo[v].distance = bfsInfo[u].distance + 1;
                // Set predecessor to vertex u
                bfsInfo[v].predecessor = u;
                // Enqueue vertex v
                queue.enqueue(v);
            }
        }
    }
    return bfsInfo;
}

exports.doBFS = doBFS;