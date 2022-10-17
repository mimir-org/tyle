import { ComponentMeta, ComponentStory } from "@storybook/react";
import { RegisterProcessing } from "./RegisterProcessing";

export default {
  title: "Content/Forms/Auth/RegisterProcessing",
  component: RegisterProcessing,
} as ComponentMeta<typeof RegisterProcessing>;

const Template: ComponentStory<typeof RegisterProcessing> = () => <RegisterProcessing />;

export const Default = Template.bind({});
