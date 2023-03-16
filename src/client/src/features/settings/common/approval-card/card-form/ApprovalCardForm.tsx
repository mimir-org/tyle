import { yupResolver } from "@hookform/resolvers/yup";
import { ApprovalCm, State } from "@mimirorg/typelibrary-types";
import { getOptionsFromEnum } from "common/utils/getOptionsFromEnum";
import { Button } from "complib/buttons";
import { Form, FormField } from "complib/form";
import { Input, Select } from "complib/inputs";
import { Controller, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import {
  useApprovalToasts,
  findNextState,
} from "features/settings/common/approval-card/card-form/ApprovalCardForm.helpers";
import { approvalSchema } from "features/settings/common/approval-card/card-form/approvalSchema";
import { FormApproval } from "features/settings/common/approval-card/card-form/types/formApproval";
import { Flexbox } from "../../../../../complib/layouts";
import { theme } from "../../../../../complib/core";

export interface ApprovalCardFormProps {
  item: ApprovalCm;
  formId?: string;
  onSubmit?: () => void;
  onReject?: (id: number, state: State, objectType: string) => void;
  showSubmitButton?: boolean;
}

export const ApprovalCardForm = ({
  item,
  formId,
  onSubmit,
  onReject,
  showSubmitButton = true,
}: ApprovalCardFormProps) => {
  const { t } = useTranslation(["settings"]);

  const stateOptions = getOptionsFromEnum<State>(State);
  const nextState = findNextState(item.state);
  const currentState = stateOptions.find((x) => x.value == nextState);
  const oldState = stateOptions.find((x) => x.value == item.state);

  const { register, control, handleSubmit, formState } = useForm<FormApproval>({
    resolver: yupResolver(approvalSchema(t)),
    defaultValues: {
      id: item.id,
      objectType: item.objectType,
      state: currentState ?? stateOptions[0],
      companyId: item.companyId,
    },
  });

  const toast = useApprovalToasts(oldState);

  return (
    <Form
      id={formId}
      alignItems={"center"}
      onSubmit={handleSubmit((data) => toast(item.id, data).then(() => onSubmit && onSubmit()))}
    >
      <Input type={"hidden"} value={item.id} {...register("id")} />
      <Input type={"hidden"} value={item.objectType} {...register("objectType")} />
      <Input type={"hidden"} value={item.companyId} {...register("companyId")} />
      <Controller
        control={control}
        name={"state"}
        render={({ field: { value, ref, ...rest } }) => (
          <FormField label={t("common.approval.stateDropdown")} error={formState.errors.state} indent={false}>
            <Select
              {...rest}
              selectRef={ref}
              placeholder={t("common.templates.select", { object: t("common.approval.stateDropdown").toLowerCase() })}
              options={stateOptions}
              value={stateOptions.find((x) => x.value === value.value)}
            />
          </FormField>
        )}
      />
      <Flexbox flexFlow="row" gap={theme.spacing.base}>
        {onReject && (
          <>
            {showSubmitButton && (
              <Button type={"button"} onClick={() => onReject(item.id, item.state, item.objectType)}>
                {t("common.approval.reject")}
              </Button>
            )}
          </>
        )}
        {showSubmitButton && <Button type={"submit"}>{t("common.approval.submit")}</Button>}
      </Flexbox>
    </Form>
  );
};
