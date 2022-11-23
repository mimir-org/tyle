import { yupResolver } from "@hookform/resolvers/yup";
import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { getOptionsFromEnum } from "common/utils/getOptionsFromEnum";
import { Button } from "complib/buttons";
import { Form, FormField } from "complib/form";
import { Input, Select } from "complib/inputs";
import { useAddUserPermission } from "external/sources/authorize/authorize.queries";
import { AccessCardProps } from "features/settings/access/card/AccessCard";
import { usePermissionToasts } from "features/settings/access/card/form/AccessCardForm.helpers";
import { accessSchema } from "features/settings/access/card/form/accessSchema";
import {
  FormUserPermission,
  mapFormUserPermissionToApiModel,
} from "features/settings/access/card/form/types/formUserPermission";
import { Controller, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";

type AccessCardFormProps = AccessCardProps;

export const AccessCardForm = ({ user }: AccessCardFormProps) => {
  const { t } = useTranslation();

  const permissions = getOptionsFromEnum<MimirorgPermission>(MimirorgPermission);
  const { register, control, handleSubmit, formState } = useForm<FormUserPermission>({
    resolver: yupResolver(accessSchema(t)),
    defaultValues: {
      userId: user.id,
      companyId: user.companyId,
      permission: permissions[0],
    },
  });

  const mutation = useAddUserPermission();
  const toast = usePermissionToasts();

  return (
    <Form
      alignItems={"center"}
      onSubmit={handleSubmit((data) =>
        toast(user.firstName, data, mutation.mutateAsync(mapFormUserPermissionToApiModel(data)))
      )}
    >
      <Input type={"hidden"} {...register("userId")} />
      <Input type={"hidden"} {...register("companyId")} />
      <Controller
        control={control}
        name={"permission"}
        render={({ field: { value, ref, ...rest } }) => (
          <FormField label={t("settings.access.permission")} error={formState.errors.permission} indent={false}>
            <Select
              {...rest}
              selectRef={ref}
              placeholder={t("common.templates.select", { object: t("settings.access.permission").toLowerCase() })}
              options={permissions}
              value={permissions.find((x) => x.value === value.value)}
            />
          </FormField>
        )}
      />
      <Button type={"submit"}>{t("settings.access.submit")}</Button>
    </Form>
  );
};
