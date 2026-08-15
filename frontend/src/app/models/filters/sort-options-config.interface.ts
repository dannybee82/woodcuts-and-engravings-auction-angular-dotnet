export type SortOption = 'PRICE_ASC' | 'PRICE_DESC' | 'BIDS_ASC' | 'BIDS_DESC' | 'CLOSING_SOON' | null;

export interface SortOptionConfig {
  value: SortOption;
  label: string;
}