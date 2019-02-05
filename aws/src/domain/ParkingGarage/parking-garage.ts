import {Serialised, TinyType, TinyTypeOf} from "tiny-types";
import { v4 as uuid } from 'uuid';


export class ParkingGarage extends TinyType {
    parkingSpaces: ParkingSpaces;

    public static fromJSON = (o: Serialised<ParkingGarage>) => new ParkingGarage(
        UUID.fromJSON(o.id as any),
        o.day as string,
        o.capacity as number,
        o.free as number
    );

    constructor(public readonly id: UUID, public readonly day: string, public readonly capacity: number, public readonly free: number) {
        super();
        this.parkingSpaces = new ParkingSpaces(new Capacity(capacity), new Capacity(free));
    }

    public reserveParkingSpace() {
        this.parkingSpaces = this.parkingSpaces.occupySpace();
    }

    public occupyParkingSpace() {
        this.parkingSpaces = this.parkingSpaces.occupySpace();
    }

    public freeParkingSpace() {
        this.parkingSpaces = this.parkingSpaces.freeSpace();
    }
}

export class UUID extends TinyType {
    static new = () => new UUID(uuid());
    public static fromJSON = (o: Serialised<UUID>) => new UUID(
        o.id as string,
    );

    constructor(public readonly id: string) {
        super();
    }
}

class ParkingSpaces extends TinyType {
    public static fromJSON = (o: Serialised<ParkingSpaces>) => new ParkingSpaces(
        Capacity.fromJSON(o.capacity as any),
        Capacity.fromJSON(o.free as any),
    );

    constructor(public readonly capacity: Capacity, public free: Capacity) {
        super();
    }

    currentlyAvailable(): Capacity {
        return this.free;
    }

    occupySpace(): ParkingSpaces {
        return new ParkingSpaces(this.capacity, new Capacity(this.free.capacity - 1));
    }

    freeSpace(): ParkingSpaces {
        return new ParkingSpaces(this.capacity, new Capacity(this.free.capacity + 1));
    }
}

class Capacity extends TinyType {
    public static fromJSON = (o: Serialised<Capacity>) => new Capacity(
        o.capacity as number,
    );

    constructor(public readonly capacity: number) {
        super();
        // TODO ensure()
    }
}