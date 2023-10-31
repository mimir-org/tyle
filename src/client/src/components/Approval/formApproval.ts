import { State, ApprovalCm } from "@mimirorg/typelibrary-types";
import { Option } from "common/utils/getOptionsFromEnum";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormApproval extends Omit<ApprovalCm, "state"> {
  state: Option<State>;
}

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formApproval client-only model
 */
export const mapFormApprovalToApiModel = (formApproval: FormApproval): ApprovalCm => ({
  ...formApproval,
  state: formApproval.state.value,
});
