export interface ContactRequest {
  fullName: string;
  email: string;
  city: string;
  postalCode: string;
  address: string;
  message: string | null;
}
