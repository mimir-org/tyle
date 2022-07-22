import { ComponentStory } from "@storybook/react";
import { Loader } from "./Loader";

export default {
  title: "Content/Common/Loader",
  component: Loader,
};

const Template: ComponentStory<typeof Loader> = () => <Loader />;

export const Default = Template.bind({});
