
export interface Listing {
  listingId: number;

  title: string;

  description: string;

  pricePerNight: number;

  street?: string;

  city?: string;

  country?: string;

  zipCode?: string;

  state?: string;

  bedroom: number;

  bathroom: number;

  beds: number;

  image1?: string;

  image2?: string;

  image3?: string;

  createdDateTime: Date;
}
