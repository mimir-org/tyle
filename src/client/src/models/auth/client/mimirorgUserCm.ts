import { MimirorgPermission } from "../enums/mimirorgPermission";

export interface MimirorgUserCm {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  permissions: { [key: number]: MimirorgPermission };
}
