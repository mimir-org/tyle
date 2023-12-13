import { AlertDialog, AlertDialogActionItem, AlertDialogCancelItem, Button } from "@mimirorg/component-library";
import { PencilSquare } from "@styled-icons/heroicons-outline";
import PermissionCard from "components/PermissionCard";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { UserItem } from "types/userItem";

interface PermissionDialogProps {
  user: UserItem;
  handleRoleChange: (user: UserItem, newRole: string | undefined) => void
}

/**
 * Dialog that presents the user with a form to change a user's permission level
 *
 * The dialog's open state is overridden to allow for form submission withing the context of the dialog.
 * The dialog's action is extended with the submit type and a form id so that it can submit an external component form.
 *
 * @param user
 * @param handleRoleChange
 * @constructor
 */
const PermissionDialog = ({ user, handleRoleChange }: PermissionDialogProps) => {
  const { t } = useTranslation("settings");
  const [open, setOpen] = useState(false);
  const formId = "changeUserPermission";

  const dialogContent = (
    <PermissionCard selected user={user} formId={formId} onSubmit={() => setOpen(false)} showSubmitButton={false} handleRoleChange={handleRoleChange} />
  );
  const dialogOverriddenSubmitAction: AlertDialogActionItem = {
    name: t("permissions.dialog.submit"),
    form: formId,
    type: "submit"
  };
  const dialogOverriddenCancelAction: AlertDialogCancelItem = {
    name: t("permissions.dialog.cancel"),
    onAction: () => { setOpen(false); },
  };

  return (
    <AlertDialog
      open={open}
      title={t("permissions.dialog.title")}
      description={t("permissions.dialog.description")}
      content={dialogContent}
      actions={[dialogOverriddenSubmitAction]}
      cancelAction={dialogOverriddenCancelAction}
    >
      <Button variant={"text"} icon={<PencilSquare />} iconOnly onClick={() => setOpen(true)}>
        {t("permissions.dialog.trigger", { name: user.name })}
      </Button>
    </AlertDialog>
  );
};

export default PermissionDialog;
