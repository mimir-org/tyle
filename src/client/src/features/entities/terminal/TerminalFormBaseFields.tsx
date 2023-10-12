import {
  Button,
  Flexbox,
  FormBaseFieldsContainer,
  FormField,
  Input,
  Textarea,
  Text,
  Token,
  Select,
} from "@mimirorg/component-library";
import { PlainLink } from "features/common/plain-link";
import { Controller, useFormContext, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { FormMode } from "../types/formMode";
import { TerminalFormFields, mediumInfoItem, purposeInfoItem } from "./TerminalForm.helpers";
import { FormSection } from "../common/form-section/FormSection";
import { useGetPurposes } from "external/sources/purpose/purpose.queries";
import { SelectItemDialog } from "../common/select-item-dialog/SelectItemDialog";
import { XCircle } from "@styled-icons/heroicons-outline";
import { useGetMedia } from "external/sources/medium/medium.queries";
import { getOptionsFromEnum } from "common/utils/getOptionsFromEnum";
import { Aspect } from "common/types/common/aspect";
import { Direction } from "common/types/terminals/direction";
interface TerminalFormBaseFieldsProps {
  mode?: FormMode;
  limited?: boolean;
}

/**
 * Component which contains all simple value fields of the terminal form.
 *
 * @param mode
 * @param limited
 * @constructor
 */
export const TerminalFormBaseFields = ({ mode, limited }: TerminalFormBaseFieldsProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");
  const { control, register, formState, setValue } = useFormContext<TerminalFormFields>();
  const { errors } = formState;

  const purposeQuery = useGetPurposes();
  const purposeInfoItems = purposeQuery.data?.map((p) => purposeInfoItem(p)) ?? [];
  const chosenPurpose = useWatch({ control, name: "purposeId" });

  const mediumQuery = useGetMedia();
  const mediumInfoItems = mediumQuery.data?.map((p) => mediumInfoItem(p)) ?? [];
  const chosenMedium = useWatch({ control, name: "mediumId" });

  const aspectOptions = getOptionsFromEnum<Aspect>(Aspect);
  const qualifierOptions = getOptionsFromEnum<Direction>(Direction);

  return (
    <FormBaseFieldsContainer>
      <Text variant={"display-small"}>{t("terminal.title")}</Text>
      <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.l}>
        <FormField label={t("terminal.name")} error={errors.name}>
          <Input placeholder={t("terminal.placeholders.name")} {...register("name")} disabled={limited} />
        </FormField>

        <FormField label={t("terminal.description")} error={errors.description}>
          <Textarea placeholder={t("terminal.placeholders.description")} {...register("description")} />
        </FormField>
      </Flexbox>

      <FormSection
        title={t("terminal.purpose.title")}
        action={
          !chosenPurpose && (
            <SelectItemDialog
              title={t("terminal.purpose.dialog.title")}
              description={t("terminal.purpose.dialog.description")}
              searchFieldText={t("terminal.purpose.dialog.search")}
              addItemsButtonText={t("terminal.purpose.dialog.add")}
              openDialogButtonText={t("terminal.purpose.dialog.open")}
              items={purposeInfoItems}
              onAdd={(ids) => {
                setValue("purposeId", Number(ids[0]));
              }}
              isMultiSelect={false}
            />
          )
        }
      >
        <Input {...register("purposeId")} type="hidden" />
        {chosenPurpose && (
          <Token
            variant={"secondary"}
            actionable
            actionIcon={<XCircle />}
            actionText={t("terminal.purpose.remove")}
            onAction={() => setValue("purposeId", undefined)}
            dangerousAction
          >
            {purposeInfoItems.find((x) => x.id === chosenPurpose.toString())?.name}
          </Token>
        )}
      </FormSection>

      <FormField label={t("terminal.notation")} error={errors.notation}>
        <Input placeholder={t("terminal.placeholders.notation")} {...register("notation")} disabled={limited} />
      </FormField>

      <FormField label={t("terminal.symbol")} error={errors.symbol}>
        <Input placeholder={t("terminal.placeholders.symbol")} {...register("symbol")} disabled={limited} />
      </FormField>

      <FormField label={t("terminal.aspect")} error={errors.aspect}>
        <Controller
          control={control}
          name={"aspect"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <Select
              {...rest}
              selectRef={ref}
              placeholder={t("common.templates.select", { object: t("terminal.aspect").toLowerCase() })}
              options={aspectOptions}
              getOptionLabel={(x) => x.label}
              onChange={(x) => {
                onChange(x?.value);
              }}
              value={aspectOptions.find((x) => x.value === value)}
              isClearable
            />
          )}
        />
      </FormField>

      <FormSection
        title={t("terminal.medium.title")}
        action={
          !chosenMedium && (
            <SelectItemDialog
              title={t("terminal.medium.dialog.title")}
              description={t("terminal.medium.dialog.description")}
              searchFieldText={t("terminal.medium.dialog.search")}
              addItemsButtonText={t("terminal.medium.dialog.add")}
              openDialogButtonText={t("terminal.medium.dialog.open")}
              items={mediumInfoItems}
              onAdd={(ids) => {
                setValue("mediumId", Number(ids[0]));
              }}
              isMultiSelect={false}
            />
          )
        }
      >
        <Input {...register("mediumId")} type="hidden" />
        {chosenMedium && (
          <Token
            variant={"secondary"}
            actionable
            actionIcon={<XCircle />}
            actionText={t("terminal.medium.remove")}
            onAction={() => setValue("mediumId", undefined)}
            dangerousAction
          >
            {mediumInfoItems.find((x) => x.id === chosenMedium.toString())?.name}
          </Token>
        )}
      </FormSection>

      <FormField label={t("terminal.qualifier")} error={errors.qualifier}>
        <Controller
          control={control}
          name={"qualifier"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <Select
              {...rest}
              selectRef={ref}
              placeholder={t("common.templates.select", { object: t("terminal.qualifier").toLowerCase() })}
              options={qualifierOptions}
              getOptionLabel={(x) => x.label}
              onChange={(x) => {
                onChange(x?.value);
              }}
              value={qualifierOptions.find((x) => x.value === value)}
            />
          )}
        />
      </FormField>

      <Flexbox justifyContent={"center"} gap={theme.mimirorg.spacing.xl}>
        <PlainLink tabIndex={-1} to={"/"}>
          <Button tabIndex={0} as={"span"} variant={"outlined"}>
            {t("common.cancel")}
          </Button>
        </PlainLink>
        <Button type={"submit"}>{mode === "edit" ? t("common.edit") : t("common.submit")}</Button>
      </Flexbox>
    </FormBaseFieldsContainer>
  );
};
