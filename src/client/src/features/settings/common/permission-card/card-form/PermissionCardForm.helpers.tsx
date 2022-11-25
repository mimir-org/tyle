import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { Option } from "common/utils/getOptionsFromEnum";
import { Button } from "complib/buttons";
import { toast } from "complib/data-display";
import { Text } from "complib/text";
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
  const { t } = useTranslation();
  const addMutation = useAddUserPermission();
  const undoToast = useUndoPermissionToast(oldPermission);

  return async (name: string, submission: FormUserPermission) => {
    const mutationPromise = addMutation.mutateAsync(mapFormUserPermissionToApiModel(submission));
    const permission = submission.permission.label.toLowerCase();

    return toast.promise(
      mutationPromise,
      {
        loading: t("settings.access.processing.loading", { name, permission }),
        success: (
          <>
            <Text variant={"label-large"}>{t("settings.access.processing.success", { name, permission })}</Text>
            <Button variant={"outlined"} onClick={() => undoToast(name, submission)}>
              {t("settings.access.undo.action")}
            </Button>
          </>
        ),
        error: t("settings.access.processing.error", { name, permission }),
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
const useUndoPermissionToast = (oldPermission?: Option<MimirorgPermission>) => {
  const { t } = useTranslation();
  const addMutation = useAddUserPermission();
  const removeMutation = useRemoveUserPermission();
  const shouldRevertToOldPermission = !!oldPermission;

  return (name: string, submission: FormUserPermission) => {
    const targetMutation = shouldRevertToOldPermission ? addMutation : removeMutation;
    const targetSubmission = shouldRevertToOldPermission ? { ...submission, permission: oldPermission } : submission;
    const mutationPromise = targetMutation.mutateAsync(mapFormUserPermissionToApiModel(targetSubmission));

    return toast.promise(mutationPromise, {
      loading: t("settings.access.undo.loading", { name }),
      success: t("settings.access.undo.success", { name }),
      error: t("settings.access.undo.error", { name }),
    });
  };
};
