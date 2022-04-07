import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Modal } from "./Modal";

export default {
  title: "Overlays/Modal",
  component: Modal,
  args: {
    isOpen: true,
    onExit: () => null,
  },
} as ComponentMeta<typeof Modal>;

const Template: ComponentStory<typeof Modal> = (args) => <Modal {...args}></Modal>;

export const Default = Template.bind({});

export const WithTitle = Template.bind({});
WithTitle.args = {
  title: "Title",
};

export const WithDescription = Template.bind({});
WithDescription.args = {
  ...WithTitle.args,
  description: "Description",
};

export const WithContent = Template.bind({});
WithContent.args = {
  ...WithDescription.args,
  children: (
    <div>
      <p>The modal renders its children prop inside</p>
      <ul>
        <li>Children could be simple html markup</li>
        <li>Or maybe complex custom components</li>
      </ul>
    </div>
  ),
};
