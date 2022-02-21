import { MimirorgPermissionAm } from "./mimirorgPermissionAm";

export interface MimirorgUserPermissionAm {
    userId: string;
    companyId: number;
    permissions: MimirorgPermissionAm[];
}