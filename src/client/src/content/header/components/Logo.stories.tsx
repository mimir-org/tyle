import { ComponentMeta } from "@storybook/react";
import { Logo } from "./Logo";
import { LibraryIcon } from "../../../assets/icons/modules";

export default {
  title: "Content/Header/Logo",
  component: Logo,
} as ComponentMeta<typeof Logo>;

export const Default = () => <Logo name={"Type library"} icon={LibraryIcon} />;
