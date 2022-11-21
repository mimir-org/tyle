import { Button } from "complib/buttons";
import { toast } from "complib/data-display";
import { Text } from "complib/text";
import { useRemoveUserPermission } from "external/sources/authorize/authorize.queries";
import {
  FormUserPermission,
  mapFormUserPermissionToApiModel,
} from "features/settings/access/card/form/types/formUserPermission";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

export const usePermissionToasts = () => {
  const theme = useTheme();
  const { t } = useTranslation();
  const undoToast = useUndoPermissionToast();

  return async (name: string, submission: FormUserPermission, promise: Promise<unknown>) => {
    const permission = submission.permission.label.toLowerCase();

    return toast.promise(
      promise,
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

const useUndoPermissionToast = () => {
  const { t } = useTranslation();
  const mutation = useRemoveUserPermission();

  return (name: string, submission: FormUserPermission) => {
    const permission = submission.permission.label.toLowerCase();
    const undoPromise = mutation.mutateAsync(mapFormUserPermissionToApiModel(submission));

    return toast.promise(undoPromise, {
      loading: t("settings.access.undo.loading", { name, permission }),
      success: t("settings.access.undo.success", { name, permission }),
      error: t("settings.access.undo.error", { name, permission }),
    });
  };
};
