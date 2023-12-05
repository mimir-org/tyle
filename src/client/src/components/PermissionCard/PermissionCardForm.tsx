import { Button, Form, FormField, Input, Select } from "@mimirorg/component-library";
import { useTranslation } from "react-i18next";
import { UserItem } from "types/userItem";
import { getOptionsFromEnum } from "../../utils";
import { Role } from "../../types/Role";

export interface PermissionCardFormProps {
  user: UserItem;
  formId?: string;
  onSubmit?: () => void;
  showSubmitButton?: boolean;
}

const PermissionCardForm = ({ user, formId, showSubmitButton = true }: PermissionCardFormProps) => {
  const { t } = useTranslation(["settings"]);

  const roleOptions = getOptionsFromEnum<Role>(Role);
  const currentRole = user.roles[0];
  const setRole = (newRole: string | undefined) => (console.log(newRole));

  //const toast = usePermissionToasts(currentPermission);

  return (
    <Form
      id={formId}
      alignItems={"center"}
      onSubmit={(data) => {
        console.log(data);
      }}
    >
      <Input type={"hidden"} value={user.id} />

          <FormField label={t("common.permission.permission")} indent={false}>
            <Select
              placeholder={t("common.templates.select", { object: t("common.permission.permission").toLowerCase() })}
              options={roleOptions}
              value={roleOptions?.find((x) => x.label === user.roles[0])}
              onChange={(x) => {setRole(x?.label)}}
            />
          </FormField>
      {showSubmitButton && <Button type={"submit"}>{t("common.permission.submit")}</Button>}
    </Form>
  );
};

export default PermissionCardForm;
