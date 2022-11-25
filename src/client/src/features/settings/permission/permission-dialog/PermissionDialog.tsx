import { PencilAlt } from "@styled-icons/heroicons-outline";
import { UserItem } from "common/types/userItem";
import { Button } from "complib/buttons";
import { AlertDialog, AlertDialogCancelItem } from "complib/overlays";
import { AlertDialogActionItem } from "complib/overlays/alert-dialog/components/AlertDialogAction";
import { PermissionCard } from "features/settings/common/permission-card/PermissionCard";
import { useState } from "react";
import { useTranslation } from "react-i18next";

interface PermissionDialogProps {
  user: UserItem;
}

/**
 * Dialog that presents the user with a form to change a user's permission level
 *
 * The dialog's open state is overridden to allow for form submission withing the context of the dialog.
 * The dialog's action is extended with the submit type and a form id so that it can submit an external component form.
 *
 * @param user
 * @constructor
 */
export const PermissionDialog = ({ user }: PermissionDialogProps) => {
  const { t } = useTranslation();
  const [open, setOpen] = useState(false);
  const formId = "changeUserPermission";

  const dialogContent = (
    <PermissionCard selected user={user} formId={formId} onSubmit={() => setOpen(false)} showSubmitButton={false} />
  );
  const dialogOverriddenSubmitAction: AlertDialogActionItem = {
    name: t("settings.permissions.dialog.submit"),
    form: formId,
    type: "submit",
  };
  const dialogOverriddenCancelAction: AlertDialogCancelItem = {
    name: t("settings.permissions.dialog.cancel"),
    onAction: () => setOpen(false),
  };

  return (
    <AlertDialog
      open={open}
      title={t("settings.permissions.dialog.title")}
      description={t("settings.permissions.dialog.description")}
      content={dialogContent}
      actions={[dialogOverriddenSubmitAction]}
      cancelAction={dialogOverriddenCancelAction}
    >
      <Button variant={"text"} icon={<PencilAlt />} iconOnly onClick={() => setOpen(true)}>
        {t("settings.permissions.dialog.trigger", { name: user.name })}
      </Button>
    </AlertDialog>
  );
};
