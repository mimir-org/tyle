export enum Role {
  Reader,
  Contributor,
  Reviewer,
  Administrator
}

export interface RoleItem {
  roleName: string;
  roleId: string;
}