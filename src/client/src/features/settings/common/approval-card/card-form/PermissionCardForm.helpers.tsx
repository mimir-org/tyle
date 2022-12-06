import { State } from "@mimirorg/typelibrary-types";
import { Option } from "common/utils/getOptionsFromEnum";
import { Button } from "complib/buttons";
import { toast } from "complib/data-display";
import { Text } from "complib/text";
import { usePatchNodeState } from "external/sources/node/node.queries";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { FormApproval, mapFormApprovalToApiModel } from "./types/formApproval";

/**
 * Shows a toast while a permission is being granted to the user.
 * Shows an undo action on the toast after the permission has been granted.
 *
 * @param oldState permission that the user had before being assigned a new one
 */
export const useApprovalToasts = (oldState?: Option<State>) => {
  const theme = useTheme();
  const { t } = useTranslation("settings");
  // const addMutation = useAddUserPermission();
  const undoToast = useUndoApprovalToast(oldState);
  const patcMutation = usePatchNodeState();

  return async (name: string, submission: FormApproval) => {
    const mutationPromise = patcMutation.mutateAsync(mapFormApprovalToApiModel(submission));
    const state = submission.state.label.toLowerCase();

    return toast.promise(
      mutationPromise,
      {
        loading: t("common.permission.processing.loading", { name, state }),
        success: (
          <>
            <Text variant={"label-large"}>{t("common.permission.processing.success", { name, state })}</Text>
            <Button variant={"outlined"} onClick={() => undoToast(name, submission)}>
              {t("common.permission.undo.action")}
            </Button>
          </>
        ),
        error: t("common.permission.processing.error", { name, state }),
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
 * Shows a toast while a permission grant is being reverted.
 *
 * Removes the permission that was given to the user if they had none.
 * Reverts back to the old permission level if the user has one.
 *
 * @param oldPermission permission that the user had before being assigned a new one
 */
const useUndoApprovalToast = (oldState?: Option<State>) => {
  const { t } = useTranslation("settings");
  const patcMutation = usePatchNodeState();
  const shouldRevertToOldPermission = !!oldState;

  return (name: string, submission: FormApproval) => {
    const targetSubmission = shouldRevertToOldPermission ? { ...submission, state: oldState } : submission;
    const mutationPromise = patcMutation.mutateAsync(mapFormApprovalToApiModel(targetSubmission));

    return toast.promise(mutationPromise, {
      loading: t("common.permission.undo.loading", { name }),
      success: t("common.permission.undo.success", { name }),
      error: t("common.permission.undo.error", { name }),
    });
  };
};
