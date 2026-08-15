export enum ItemType {
    ALL = "ALL",
    UNKNOWN = "UNKNOWN",
    WOODCUT = "WOODCUT",
    ENGRAVING = "ENGRAVING"    
}

export const itemTypeArr: ItemType[] = [
    ItemType.ALL,
    ItemType.WOODCUT,
    ItemType.ENGRAVING,
    ItemType.UNKNOWN
];

export const itemTypeArrForNewItems: ItemType[] = [
    ItemType.WOODCUT,
    ItemType.ENGRAVING,
    ItemType.UNKNOWN
];