import { Button } from "complib/buttons";
import { toast } from "complib/data-display";
import { Box } from "complib/layouts";
import { Text } from "complib/text";
import { useRemoveUserPermission } from "external/sources/authorize/authorize.queries";
import { MotionUndoToasterContainer } from "features/settings/access/card/form/AccessCardForm.styled";
import {
  FormUserPermission,
  mapFormUserPermissionToApiModel,
} from "features/settings/access/card/form/types/formUserPermission";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

export const usePermissionToasts = (name?: string) => {
  const theme = useTheme();
  const { t } = useTranslation();
  const addToast = useAddPermissionToast();
  const undoToast = useUndoPermissionToast();

  return async (submission: FormUserPermission, submissionPromise: Promise<unknown>) => {
    await addToast(submissionPromise, name);
    const permission = submission.permission.label.toLowerCase();

    toast.custom(
      <MotionUndoToasterContainer {...theme.tyle.animation.from("right", 400)}>
        <Box>
          <Text as={"span"} variant={"label-large"}>
            {name}
          </Text>
          <Text as={"span"} variant={"label-large"} fontWeight={theme.tyle.typography.sys.roles.body.large.weight}>
            {t("settings.access.undo.state", { permission })}
          </Text>
        </Box>
        <Button variant={"outlined"} onClick={() => undoToast(submission, name)}>
          {t("settings.access.undo.action")}
        </Button>
      </MotionUndoToasterContainer>,
      { duration: 3000 }
    );
  };
};

const useAddPermissionToast = () => {
  const { t } = useTranslation();

  return (promise: Promise<unknown>, name?: string) =>
    toast.promise(promise, {
      loading: t("settings.access.processing.loading", { name }),
      success: t("settings.access.processing.success", { name }),
      error: t("settings.access.processing.error", { name }),
    });
};

const useUndoPermissionToast = () => {
  const { t } = useTranslation();
  const mutation = useRemoveUserPermission();

  return (submission: FormUserPermission, name?: string) => {
    const undoPromise = mutation.mutateAsync(mapFormUserPermissionToApiModel(submission));

    return toast.promise(undoPromise, {
      loading: t("settings.access.undo.loading", { name }),
      success: t("settings.access.undo.success", { name }),
      error: t("settings.access.undo.error", { name }),
    });
  };
};
