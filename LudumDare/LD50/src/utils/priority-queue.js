class PriorityQueue {

    constructor() {
        this.items = [];
    }

    enqueue(item, priority) {

        var inserted = false;
        for (var i = 0; i < this.items.length; i++) {
            if (this.items[i].priority > priority) {
                this.items.splice(i, 0, { item, priority });
                inserted = true;
                break;
            }
        }

        if (!inserted) {
            this.items.push({ item, priority });
        }
    }

    dequeue() {

        if (this.isEmpty())
            return null;
        return this.items.shift();
    }

    front() {
        if (this.isEmpty())
            return null;
        return this.items[0];
    }

    back() {
        if (this.isEmpty())
            return null;
        return this.items[this.items.length - 1];
    }

    isEmpty() {
        return this.items.length == 0;
    }

}

export default PriorityQueue;
