import { ComponentMeta } from "@storybook/react";
import { User } from "./User";

export default {
  title: "Content/Header/User",
  component: User,
} as ComponentMeta<typeof User>;

export const Default = () => <User name={`Guybrush Threepwood`} />;
