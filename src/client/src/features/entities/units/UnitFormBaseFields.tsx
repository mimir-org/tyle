import { useTheme } from "styled-components";
import { useTranslation } from "react-i18next";
import { useFormContext } from "react-hook-form";
import { useGetUnits } from "../../../external/sources/unit/unit.queries";
import { Flexbox } from "../../../complib/layouts";
import { FormField } from "../../../complib/form";
import { Input, Textarea } from "../../../complib/inputs";
import { PlainLink } from "../../common/plain-link";
import { Button } from "../../../complib/buttons";
import { UnitLibAm } from "@mimirorg/typelibrary-types/index";

export default function UnitFormBaseFields() {
  const theme = useTheme();
  const { t } = useTranslation("entities");
  const { control, register, formState } = useFormContext<UnitLibAm>();
  const { errors } = formState;

  const unitQuery = useGetUnits();
  const units = unitQuery.data || [];

  return (
    <>
      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.l}>
        <FormField label={t("unit.name")} error={errors.name}>
          <Input placeholder={t("unit.placeholders.name")} {...register("name")} disabled={false} />
        </FormField>

        <FormField label={t("unit.description")} error={errors.description}>
          <Textarea placeholder={t("unit.placeholders.description")} {...register("description")} />
        </FormField>

        <Flexbox justifyContent={"center"} gap={theme.tyle.spacing.xl}>
          <PlainLink tabIndex={-1} to={"/"}>
            <Button tabIndex={0} as={"span"} variant={"outlined"} dangerousAction>
              {t("common.cancel")}
            </Button>
          </PlainLink>
          <Button type={"submit"}>{t("common.submit")}</Button>
        </Flexbox>
      </Flexbox>
    </>
  );
}
