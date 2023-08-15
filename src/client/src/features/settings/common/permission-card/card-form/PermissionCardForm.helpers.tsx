import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { Option } from "common/utils/getOptionsFromEnum";
import { toast } from "complib/data-display";
import { Button, Text } from "@mimirorg/component-library";
import { useAddUserPermission, useRemoveUserPermission } from "external/sources/authorize/authorize.queries";
import {
  FormUserPermission,
  mapFormUserPermissionToApiModel,
} from "features/settings/common/permission-card/card-form/types/formUserPermission";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

/**
 * Shows a toast while a permission is being granted to the user.
 * Shows an undo action on the toast after the permission has been granted.
 *
 * @param oldPermission permission that the user had before being assigned a new one
 */
export const usePermissionToasts = (oldPermission?: Option<MimirorgPermission>) => {
  const theme = useTheme();
  const { t } = useTranslation("settings");
  const addMutation = useAddUserPermission();
  const undoToast = useUndoPermissionToast(oldPermission);

  return async (name: string, submission: FormUserPermission) => {
    const mutationPromise = addMutation.mutateAsync(mapFormUserPermissionToApiModel(submission));
    const permission = submission.permission.label.toLowerCase();

    return toast.promise(
      mutationPromise,
      {
        loading: t("common.permission.processing.loading", { name, permission }),
        success: (
          <>
            <Text variant={"label-large"}>{t("common.permission.processing.success", { name, permission })}</Text>
            <Button variant={"outlined"} onClick={() => undoToast(name, submission)}>
              {t("common.permission.undo.action")}
            </Button>
          </>
        ),
        error: t("common.permission.processing.error", { name, permission }),
      },
      {
        success: {
          style: {
            backgroundColor: theme.mimirorg.color.warning.base,
          },
        },
      },
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
const useUndoPermissionToast = (oldPermission?: Option<MimirorgPermission>) => {
  const { t } = useTranslation("settings");
  const addMutation = useAddUserPermission();
  const removeMutation = useRemoveUserPermission();
  const shouldRevertToOldPermission = !!oldPermission;

  return (name: string, submission: FormUserPermission) => {
    const targetMutation = shouldRevertToOldPermission ? addMutation : removeMutation;
    const targetSubmission = shouldRevertToOldPermission ? { ...submission, permission: oldPermission } : submission;
    const mutationPromise = targetMutation.mutateAsync(mapFormUserPermissionToApiModel(targetSubmission));

    return toast.promise(mutationPromise, {
      loading: t("common.permission.undo.loading", { name }),
      success: t("common.permission.undo.success", { name }),
      error: t("common.permission.undo.error", { name }),
    });
  };
};
