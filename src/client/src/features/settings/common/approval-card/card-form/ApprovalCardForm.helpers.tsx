import { State } from "@mimirorg/typelibrary-types";
import { Option } from "common/utils/getOptionsFromEnum";
import { Button } from "complib/buttons";
import { toast } from "complib/data-display";
import { Text } from "complib/text";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import {
  FormApproval,
  mapFormApprovalToApiModel,
} from "features/settings/common/approval-card/card-form/types/formApproval";
import { Flexbox } from "complib/layouts/Flexbox";
import { usePatchTerminalState } from "external/sources/terminal/terminal.queries";
import { usePatchTransportState } from "external/sources/transport/transport.queries";
import { usePatchInterfaceState } from "external/sources/interface/interface.queries";
import { usePatchNodeState } from "external/sources/node/node.queries";
import { ApprovalDataCm } from "common/types/approvalDataCm";

/**
 * Shows a toast while a approval is sent to server.
 * Shows an undo action on the toast after the approval is sent.
 *
 * @param oldState state that the approval had before beeing approved
 */
export const useApprovalToasts = (oldState?: Option<State>) => {
  const theme = useTheme();
  const { t } = useTranslation("settings");
  const undoToast = useUndoApprovalToast(oldState);
  const patcMutationNode = usePatchNodeState();
  const patcMutationTerminal = usePatchTerminalState();
  const patcMutationTransport = usePatchTransportState();
  const patcMutationInterface = usePatchInterfaceState();

  let mutationPromise = {} as Promise<ApprovalDataCm>;

  return async (name: string, submission: FormApproval) => {
    switch (submission.objectType) {
      case "Node":
        mutationPromise = patcMutationNode.mutateAsync(mapFormApprovalToApiModel(submission));
        break;
      case "Terminal":
        mutationPromise = patcMutationTerminal.mutateAsync(mapFormApprovalToApiModel(submission));
        break;
      case "Interface":
        mutationPromise = patcMutationInterface.mutateAsync(mapFormApprovalToApiModel(submission));
        break;
      case "Transport":
        mutationPromise = patcMutationTransport.mutateAsync(mapFormApprovalToApiModel(submission));
        break;
      default:
        throw new Error("Can't");
    }

    return toast.promise(
      mutationPromise,
      {
        loading: t("common.approval.processing.loading"),
        success: (
          <Flexbox alignContent="center" alignItems="center">
            <Text variant={"label-large"} mr={theme.tyle.spacing.base}>
              {t("common.approval.processing.success")}
            </Text>
            <Button variant={"outlined"} onClick={() => undoToast(name, submission)}>
              {t("common.approval.undo.action")}
            </Button>
          </Flexbox>
        ),
        error: t("common.approval.processing.error"),
      },
      {
        success: {
          style: {
            backgroundColor: theme.tyle.color.sys.warning.base,
          },
        },
      }
    );
  };
};

/**
 * Shows a toast while an approval is being reverted.
 *
 * Reverts the approval.
 * Reverts back to the old approval.
 *
 * @param oldState state that the approval had before beeing approved
 */
const useUndoApprovalToast = (oldState?: Option<State>) => {
  const { t } = useTranslation("settings");
  const patcMutationNode = usePatchNodeState();
  const patcMutationTerminal = usePatchTerminalState();
  const patcMutationTransport = usePatchTransportState();
  const patcMutationInterface = usePatchInterfaceState();
  const shouldRevertToOldApproval = !!oldState;

  return (name: string, submission: FormApproval) => {
    const targetSubmission = shouldRevertToOldApproval ? { ...submission, state: oldState } : submission;
    let mutationPromise = {} as Promise<ApprovalDataCm>;

    switch (submission.objectType) {
      case "Node":
        mutationPromise = patcMutationNode.mutateAsync(mapFormApprovalToApiModel(targetSubmission));
        break;
      case "Terminal":
        mutationPromise = patcMutationTerminal.mutateAsync(mapFormApprovalToApiModel(targetSubmission));
        break;
      case "Interface":
        mutationPromise = patcMutationInterface.mutateAsync(mapFormApprovalToApiModel(targetSubmission));
        break;
      case "Transport":
        mutationPromise = patcMutationTransport.mutateAsync(mapFormApprovalToApiModel(targetSubmission));
        break;
      default:
        throw new Error("Can't");
    }

    return toast.promise(mutationPromise, {
      loading: t("common.approval.undo.loading"),
      success: t("common.approval.undo.success"),
      error: t("common.approval.undo.error"),
    });
  };
};

/**
 * Find the next state from current state.
 * @param state current state
 */
export const findNextState = (state: State): State => {
  switch (state) {
    case State.ApproveCompany:
      return State.ApprovedCompany;
    case State.ApproveGlobal:
      return State.ApprovedGlobal;
    case State.Delete:
      return State.Deleted;
    default:
      return state;
  }
};
