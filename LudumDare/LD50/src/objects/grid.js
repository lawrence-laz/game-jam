import PriorityQueue from "../utils/priority-queue.js";
import Box from "./box.js";
import Hero from "./hero.js";
import Imp from "./imp.js";
import Spikes from "./spikes.js";

class Grid extends Phaser.GameObjects.Sprite {

    constructor(scene, x = 0, y = 0, texture = 'grid') {
        super(scene, x, y, texture)
        this.scene = scene;
        this.setOrigin(0, 0);
        this.cells = [
            [null, null, null, null, null, null, null, null, null, null, null, null],
            [null, null, null, null, null, null, null, null, null, null, null, null],
            [null, null, null, null, null, null, null, null, null, null, null, null],
            [null, null, null, null, null, null, null, null, null, null, null, null],
            [null, null, null, null, null, null, null, null, null, null, null, null],
            [null, null, null, null, null, null, null, null, null, null, null, null],
            [null, null, null, null, null, null, null, null, null, null, null, null],
            [null, null, null, null, null, null, null, null, null, null, null, null],
            [null, null, null, null, null, null, null, null, null, null, null, null],
            [null, null, null, null, null, null, null, null, null, null, null, null],
        ];

        this.cellSize = 16;
        this.width = 10;
        this.height = 12;

        scene.add.existing(this)
        scene.events.on('update', this.update, this)

        scene.time.addEvent({
            delay: 500,
            callback: () => this.updateTick(),
            loop: true
        });

        this.createGates();

    }

    createGates() {
        for (var i = 0; i < this.cells.length; ++i) {
            let texture = i == 4
                ? 'left-gate'
                : i == 5
                    ? 'right-gate'
                    : 'fence';
            let object = new Box(this.scene, 0, 0, texture);
            this.setCellTo(object, i, this.height - 1);
            this.scene.objects.add(object);
            object.health = i == 4 || i == 5
                ? 5
                : 10;
            object.setName('fence');
        }
    }

    update(time, delta) {

    }

    findPathDijkstra(from, to) {

        let frontier = new PriorityQueue();
        frontier.enqueue(from, 0);
        let cameFrom = new Map();
        let costSoFar = new Map();
        let fromHash = this.hash(from);
        cameFrom[fromHash] = null;
        costSoFar[fromHash] = 0;
        let pathFound = false;
        
        while (!frontier.isEmpty()) {

            let { item, priority } = frontier.dequeue();
            let current = item;
            if (current.equals(to)) {
                pathFound = true;
                break;
            }

            let neighbors = this.getNeighbors(current);
            for (var i = 0; i < neighbors.length; ++i) {

                let next = neighbors[i];
                let newCost = costSoFar[this.hash(current)] + this.getTraversalPrice(next);
                let nextHash = this.hash(next);
                if (!costSoFar[nextHash] || newCost < costSoFar[nextHash]) {

                    costSoFar[nextHash] = newCost;
                    let priority = newCost;
                    frontier.enqueue(next, priority);
                    cameFrom[nextHash] = current;
                }
            }
            
        }

        if (!pathFound) {
            return null;
        }

        let path = [];
        let nextCell = to;
        while (!nextCell.equals(from)) {
            path.push(nextCell);
            let nextCallHash = this.hash(nextCell);
            nextCell = cameFrom[nextCallHash];
        }
        path.push(nextCell);

        return path;
    }

    hash(vector) {
        // Danger zone, this has function is only good for this particular grid.
        let hash = vector.x * 1000 + vector.y;

        return hash;
    }

    findPath(from, to) {

        var { distance, path } = this.findPathRecursion(from, to, new Phaser.Math.Vector2(-1, -1));

        return { distance, path };
    }

    getTraversalPrice(cell) {
        if (cell.x >= 0 && cell.x < this.width
            && cell.y == this.height) {
            return 0; // Beyond the fence.
        }

        let cellObject = this.cells[cell.x][cell.y];

        if (cellObject == null) {
            return 1;
        } else if (cellObject.health) {
            return cellObject.health;
        } else {
            return Infinity;
        }
    }

    findPathRecursion(from, to, previousCell) {
        let neighbors = this.getNeighbors(from);
        let shortestDistance = Infinity;
        let shortestPath = null;

        // visitedCells.push(from);
        for (var i = 0; i < neighbors.length; ++i) {

            let neighbor = neighbors[i];
            // if (this.includes(visitedCells, neighbor)) {
            //     continue;
            // }
            if (neighbor.equals(previousCell)) {
                continue;
            }
            if (neighbor.equals(to)) {
                shortestDistance = this.getTraversalPrice(neighbor);
                shortestPath = [neighbor];
                break;
            }

            let { distance, path } = this.findPathRecursion(neighbor, to, from);
            if (distance < shortestDistance) {
                shortestDistance = distance;
                shortestPath = path;
            }
        }
        // visitedCells.pop(from);

        if (shortestPath != null) {
            shortestDistance += this.getTraversalPrice(from);
            shortestPath.push(from);
        }

        return { distance: shortestDistance, path: shortestPath };
    }

    includes(array, item) {
        for (var i = 0; i < array.length; ++i) {
            if (array[i].equals(item)) {
                return true;
            }
        }

        return false;
    }

    getNeighbors(center) {

        let potentialNeighbors = [
            center.clone().add(new Phaser.Math.Vector2(-1, 0)),
            center.clone().add(new Phaser.Math.Vector2(1, 0)),
            center.clone().add(new Phaser.Math.Vector2(0, 1)),
        ];

        let neighbors = [];
        for (var i = 0; i < potentialNeighbors.length; ++i) {
            let potentialNeighbor = potentialNeighbors[i];
            if (potentialNeighbor.x >= 0 && potentialNeighbor.x < this.width
                && potentialNeighbor.y >= 0 && potentialNeighbor.y <= this.height) {

                neighbors.push(potentialNeighbor);
            }
        }

        return neighbors;
    }

    updateTick() {

        // debugger;

        for (var x = 0; x < this.cells.length; ++x)
            for (var y = this.cells[0].length - 1; y >= 0; --y) {

                let cellObject = this.cells[x][y];
                if (cellObject == null || cellObject instanceof Hero) {
                    continue;
                } else if (cellObject.active === false) {
                    this.cells[x][y] = null;
                    continue;
                }

                cellObject.isStable = !this.tryMoveTo(cellObject, x, y, x, y + 1);

            }
    }

    setCellTo(object, x, y) {
        this.cells[x][y] = object;
        let position = this.getPositionForCell(x, y);
        object.x = position.x;
        object.y = position.y;
    }

    tryMoveTo(movingObject, beforeX, beforeY, afterX, afterY) {

        if (movingObject instanceof Imp 
            && afterY == this.height
            && beforeY == this.height - 1) {
            // TODO: Maybe health like in TD?
            alert('The gates were breached!');
            this.scene.scene.restart();
        }

        if (afterX < 0 || afterX >= this.cells.length
            || afterY < 0 || afterY >= this.cells[0].length) {
            return false;
        }

        let targetObject = this.cells[afterX][afterY];
        if (targetObject != null) {
            if (!this.trySquish(movingObject, afterX, afterY)) {
                return false;
            }
        }

        if (beforeX >= 0 && beforeX < this.cells.length
            && beforeY >= 0 && beforeY < this.cells[0].length) {
            this.cells[beforeX][beforeY] = null;
        }
        this.cells[afterX][afterY] = movingObject;

        let afterPosition = this.getPositionForCell(afterX, afterY);

        if (!(movingObject instanceof Hero)) {
            var tween = this.scene.tweens.add({
                targets: movingObject,
                x: afterPosition.x,
                y: afterPosition.y,
                ease: 'Power2',
                // paused: true
            });
        }

        return true;
    }

    trySquish(source, x, y) {
        let cellObject = this.cells[x][y];
        if (cellObject == null || cellObject == source) {

            return false;

        } else if (cellObject instanceof Hero) {

            if (this.getCellObject(x, y + 1) == null) {

                // Push the hero.
                this.tryMoveTo(cellObject, x, y, x, y + 1);

            } else {

                // Squishidy squish.
                cellObject.destroy();
                alert("You got squished!");
                this.scene.scene.restart();
                // TODO: proper game over;
                return true;
            }
        } else if (cellObject instanceof Spikes) {
            return this.tryGetSpiked(source, cellObject, x, y);
        }
    }

    tryGetSpiked(source, spikes, x, y) {
        if (source instanceof Box) {
            return false;
        } else {
            // Get spiked.
            spikes.onHit();
            if (source.onDestroy) {
                source.onDestroy();
            }
            source.destroy();
            return false;
        }
    }

    getCellObject(x, y) {

        if (x < 0 || x >= this.cells.length
            || y < 0 || y >= this.cells[0].length) {
            return null;
        }

        let cellObject = this.cells[x][y];
        return cellObject;
    }

    tryMoveOffset(object, offsetX) {

        let beforeCell = this.getCellForPosition(object.x, object.y);
        let objectInGrid = this.cells[beforeCell.x][beforeCell.y];
        if (objectInGrid != object) {
            beforeCell.y += 1;
            objectInGrid = this.cells[beforeCell.x][beforeCell.y];
            if (objectInGrid != object) {
                return;
            }
        }

        let targetCell = this.getCellForPosition(object.x, object.y);
        targetCell.x += offsetX;
        let targetPosition = this.getPositionForCell(targetCell.x, targetCell.y);
        let threshold = 3; // Moving this amount up is acceptable.
        if (object.y > targetPosition.y + threshold) {
            // Avoid moving up.
            targetCell.y += 1;
        }

        this.tryMoveTo(
            object,
            beforeCell.x, beforeCell.y,
            targetCell.x, targetCell.y);
    }

    getCellForPosition(x, y) {
        let cell = new Phaser.Math.Vector2(
            Phaser.Math.FloorTo(x / this.cellSize),
            Phaser.Math.FloorTo(y / this.cellSize));

        return cell;
    }

    getPositionForCell(x, y) {
        let position = new Phaser.Math.Vector2(
            x * this.cellSize,
            y * this.cellSize);

        return position;
    }

    getAlignedPosition(x, y) {
        let cell = new Phaser.Math.Vector2(
            Phaser.Math.RoundTo(x / this.cellSize),
            Phaser.Math.RoundTo(y / this.cellSize));
        let alignedPosition = this.getPositionForCell(cell.x, cell.y);

        return alignedPosition;
    }
}

export default Grid;
