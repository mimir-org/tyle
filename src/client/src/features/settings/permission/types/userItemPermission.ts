import { MimirorgPermission } from "@mimirorg/typelibrary-types";

export enum MimirorgPermissionExtended {
  All = -1,
}

export type UserItemPermission = MimirorgPermissionExtended | MimirorgPermission;
