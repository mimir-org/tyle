import { State } from "@mimirorg/typelibrary-types";
import { ApprovalAm } from "common/types/approvalAm";
import { Option } from "common/utils/getOptionsFromEnum";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormApproval extends Omit<ApprovalAm, "state"> {
  state: Option<State>;
}

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formApproval client-only model
 */
export const mapFormApprovalToApiModel = (formApproval: FormApproval): ApprovalAm => ({
  ...formApproval,
  state: formApproval.state.value,
});
