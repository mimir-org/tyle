import { Button, Form, FormField, Input, Select } from "@mimirorg/component-library";
import { useTranslation } from "react-i18next";
import { UserItem } from "types/userItem";
import { getOptionsFromEnum } from "../../utils";
import { Role } from "../../types/Role";

export interface PermissionCardFormProps {
  user: UserItem;
  formId?: string;
  showSubmitButton?: boolean;
  selectedRole: string;
  setSelectedRole: (role: string) => void;
}
const PermissionCardForm = ({ user, formId, showSubmitButton = true, setSelectedRole, selectedRole }: PermissionCardFormProps) => {
  //TODO Remove translation
  const { t } = useTranslation(["settings"]);
  const roleOptions = getOptionsFromEnum<Role>(Role);

  return (
    <Form
      id={formId}
      alignItems={"center"}
    >
      <Input type={"hidden"} value={user.id} />
      <FormField label={t("common.permission.permission")} indent={false}>
        <Select
          placeholder={t("common.templates.select", { object: t("common.permission.permission").toLowerCase() })}
          options={roleOptions}
          value={roleOptions?.find((x) => x.label === selectedRole)}
          onChange={(x) => {setSelectedRole(x!.label)}}
        />
      </FormField>
      {showSubmitButton && <Button type={"submit"}>{t("common.permission.submit")}</Button>}
    </Form>
);
};

export default PermissionCardForm;
