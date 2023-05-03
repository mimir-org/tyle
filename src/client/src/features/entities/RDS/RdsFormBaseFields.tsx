import { FormField } from "../../../complib/form";
import { Input, Textarea } from "../../../complib/inputs";
import { useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { RdsLibAm } from "@mimirorg/typelibrary-types";

export const RdsFormBaseFields = () => {
  const { register, formState } = useFormContext<RdsLibAm>();
  const { errors } = formState;
  const { t } = useTranslation("entities");

  return (
    <>
      <FormField label={t("rds.rdsCode")} error={errors.rdsCode}>
        <Input placeholder={t("rds.placeholders.rdsCode")} {...register("rdsCode")} required />
      </FormField>

      <FormField label={t("rds.name")} error={errors.name}>
        <Input placeholder={t("rds.name")} {...register("name")} required />
      </FormField>

      <FormField label={t("rds.typeReference")} error={errors.typeReference}>
        <Input placeholder={t("rds.typeReference")} {...register("typeReference")} required />
      </FormField>

      <FormField label={t("rds.description")} error={errors.description}>
        <Textarea placeholder={t("rds.description")} {...register("description")} />
      </FormField>
    </>
  );
};
