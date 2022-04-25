import { ComponentMeta } from "@storybook/react";
import { Token } from "./Token";

export default {
  title: "General/Token",
  component: Token,
} as ComponentMeta<typeof Token>;

export const Small = () => <Token text={"Small"} variant={"small"} />;

export const Medium = () => <Token text={"Medium (default)"} />;

export const Large = () => <Token text={"Large"} variant={"large"} />;
