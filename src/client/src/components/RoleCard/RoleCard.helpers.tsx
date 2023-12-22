import { UserItem } from "types/userItem";

export const useUserDescriptors = (user: UserItem): { [key: string]: string } => {
  const descriptors: { [key: string]: string } = {};

  descriptors["E-mail"] = user.email;

  if (user.purpose) {
    descriptors["Purpose"] = user.purpose;
  }

  return descriptors;
};
