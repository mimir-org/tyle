import { DevTool } from "@hookform/devtools";
import { Button, Form, FormField } from "@mimirorg/component-library";
import { useUpdateUser } from "api/user.queries";
import Input from "components/Input";
import Loader from "components/Loader";
import { onSubmitForm, usePrefilledForm } from "helpers/form.helpers";
import { useNavigateOnCriteria } from "hooks/useNavigateOnCriteria";
import { useServerValidation } from "hooks/useServerValidation";
import { useForm } from "react-hook-form";
import { UserRequest } from "types/authentication/userRequest";
import { UserView } from "types/authentication/userView";
import {
  addDummyPasswordToUserAm,
  mapUserViewToRequest,
  useUpdatingToast,
  useUserQuery,
} from "./userSettingsForm.helpers";

interface UserSettingsFormProps {
  defaultValues?: UserRequest;
}

const UserSettingsForm = ({ defaultValues }: UserSettingsFormProps) => {
  const formMethods = useForm<UserRequest>({
    defaultValues: defaultValues,
    //resolver: yupResolver(userSchema(t)),
  });

  const { register, handleSubmit, control, setError, reset, formState } = formMethods;

  const query = useUserQuery();
  const mapper = (userCm: UserView) => mapUserViewToRequest(userCm);
  const [_, isLoading] = usePrefilledForm(query, mapper, reset);

  const mutation = useUpdateUser();
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useUpdatingToast();

  return (
    <Form onSubmit={handleSubmit((data) => onSubmitForm(addDummyPasswordToUserAm(data), mutation.mutateAsync, toast))}>
      {isLoading && <Loader />}
      {!isLoading && (
        <>
          <FormField label="First name" error={formState.errors.firstName}>
            <Input style={{ fontSize: "17px" }} placeholder="Enter first name" {...register("firstName")} />
          </FormField>
          <FormField label="Last name" error={formState.errors.lastName}>
            <Input style={{ fontSize: "17px" }} placeholder="Enter last name" {...register("lastName")} />
          </FormField>
          <Button type={"submit"}>Update user settings</Button>
        </>
      )}
      <DevTool control={control} placement={"bottom-right"} />
    </Form>
  );
};

export default UserSettingsForm;
