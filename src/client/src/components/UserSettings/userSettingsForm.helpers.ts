import { useGetCurrentUser } from "api/user.queries";
import { toast } from "components/Toaster/toast";
import { UserRequest } from "types/authentication/userRequest";
import { UserView } from "types/authentication/userView";

export const useUserQuery = () => {
  return useGetCurrentUser();
};

export const mapUserViewToRequest = (user: UserView): UserRequest => ({
  email: user.email,
  password: "",
  confirmPassword: "",
  firstName: user.firstName,
  lastName: user.lastName,
  purpose: user.purpose,
});

export const useUpdatingToast = () => {
  return (updatingPromise: Promise<unknown>) =>
    toast.promise(updatingPromise, {
      loading: "Updating user settings",
      success: "Your user settings have been updated",
      error: "An error occured while updating your user settings",
    });
};

export const addDummyPasswordToUserAm = (user: UserRequest): UserRequest => ({
  ...user,
  password: "DummyPassword1234",
  confirmPassword: "DummyPassword1234",
});
