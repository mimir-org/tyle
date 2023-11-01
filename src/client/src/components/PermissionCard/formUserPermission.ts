import { MimirorgPermission, MimirorgUserPermissionAm } from "@mimirorg/typelibrary-types";
import { Option } from "utils";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormUserPermission extends Omit<MimirorgUserPermissionAm, "permission"> {
  permission: Option<MimirorgPermission>;
}

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formUserPermission client-only model
 */
export const mapFormUserPermissionToApiModel = (formUserPermission: FormUserPermission): MimirorgUserPermissionAm => ({
  ...formUserPermission,
  permission: formUserPermission.permission.value,
});
